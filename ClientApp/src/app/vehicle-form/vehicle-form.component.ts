import { Component, OnInit } from '@angular/core';
import { VehicleService } from '../../services/vehicle.service';

interface IMake {
  id: number | string;
  name: string;
  models: IModel[];
}

interface IModel {
  id: number;
  name: string;
}

interface IFeature {
  id: number;
  name: string;
}

interface IVehicle {
  make: IMake;
  model: IModel;
  features: IFeature[];
}

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css'],
})
export class VehicleFormComponent implements OnInit {
  makes: IMake[];
  models: IModel[];
  features: any[];
  vehicle: IVehicle = {
    make: { id: 0, name: '', models: [] },
    model: { id: 0, name: '' },
    features: [],
  };
  enableModelSelection = false;

  constructor(private vehicleService: VehicleService) {}

  ngOnInit() {
    this.vehicleService
      .getMakes()
      .subscribe((makes) => (this.makes = makes as IMake[]));

    this.vehicleService
      .getFeatures()
      .subscribe((features) => (this.features = features as IFeature[]));
  }

  onMakeChange() {
    const make = this.makes.find(
      (m: IMake) => m.id.toString() === this.vehicle.make.id
    );

    if (!make || make.id === 0) {
      this.enableModelSelection = false;
      return;
    }

    this.enableModelSelection = true;
    this.vehicle.make = { ...make };

    this.vehicleService.getOneMake(make.id).subscribe((m: IMake) => {
      return (this.models = m.models as IModel[]);
    });
  }

  onModelChange() {
    const model = this.models.find(
      (m) => m.id.toString() === this.vehicle.make.id
    );

    if (!model || model.id === 0) {
      return;
    }

    this.vehicle.model = model;
  }
}
