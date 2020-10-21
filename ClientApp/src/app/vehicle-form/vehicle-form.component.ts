import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastyService } from 'ng2-toasty';
import { forkJoin } from 'rxjs';
import { VehicleService } from '../../services/vehicle.service';
import { ISaveVehicle, IVehicle } from '../IVehicle';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css'],
})
export class VehicleFormComponent implements OnInit {
  makes: any;
  models: any;
  features: any;
  vehicle: ISaveVehicle = {
    id: 0,
    makeId: 0,
    modelId: 0,
    isRegistered: false,
    features: [],
    contact: {
      email: '',
      name: '',
      phone: '',
    },
  };

  constructor(
    private vehicleService: VehicleService,
    private toastyService: ToastyService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    route.params.subscribe((p) => {
      this.vehicle.id = +p['id'];
    });
  }

  ngOnInit() {
    const dataSources = [
      this.vehicleService.getMakes(),
      this.vehicleService.getFeatures(),
    ];

    if (this.vehicle.id) {
      dataSources.push(this.vehicleService.getVehicle(this.vehicle.id));
    }

    forkJoin(dataSources).subscribe(
      (data) => {
        this.makes = data[0];
        this.features = data[1];

        if (this.vehicle.id) {
          this.setVehicle(data[2] as IVehicle);
          this.populateModels();
        }
      },
      (err) => {
        if (err.status == 404) {
          console.log(err);
          this.router.navigate(['']);
        }
      }
    );
  }

  private setVehicle(v: IVehicle) {
    console.log(v);
    this.vehicle.id = v.id;
    this.vehicle.makeId = v.make.id;
    this.vehicle.modelId = v.model.id;
    this.vehicle.isRegistered = v.isRegistered;
    this.vehicle.contact = v.contact;
    this.vehicle.features = v.features.map((f) => f.id);
  }

  onMakeChange() {
    this.populateModels();
    delete this.vehicle.modelId;
  }

  private populateModels() {
    const selectedMake = this.makes.find((m) => m.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
  }

  onFeatureToggle(id, $event) {
    if ($event.target.checked) {
      this.vehicle.features.push(id);
    } else {
      const index = this.vehicle.features.indexOf(id);
      this.vehicle.features.splice(index, 1);
    }
  }

  submit() {
    this.vehicleService.create(this.vehicle).subscribe((x) => console.log(x));
  }

  delete() {
    if (confirm('Are you sure?')) {
      this.vehicleService.delete(this.vehicle.id).subscribe((x) => {
        this.router.navigate(['']);
      });
    }
  }
}
