import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastyService } from 'ng2-toasty';
import { PhotoService } from '../../services/photo.service';
import { VehicleService } from '../../services/vehicle.service';
import { IVehicle } from '../../types';

@Component({
  selector: 'app-view-vehicle',
  templateUrl: './view-vehicle.component.html',
  styleUrls: ['./view-vehicle.component.css'],
})
export class ViewVehicleComponent implements OnInit {
  vehicle: IVehicle;
  vehicleId: number;
  photos: any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toasty: ToastyService,
    private vehicleService: VehicleService,
    private photoService: PhotoService
  ) {
    route.params.subscribe((p) => {
      this.vehicleId = +p['id'];
      if (isNaN(this.vehicleId) || this.vehicleId <= 0) {
        router.navigate(['/vehicles']);
        return;
      }
    });
  }

  ngOnInit() {
    this.photoService
      .getPhotos(this.vehicleId)
      .subscribe((photos) => (this.photos = photos));

    this.vehicleService.getVehicle(this.vehicleId).subscribe(
      (v) => (this.vehicle = v as IVehicle),
      (err) => {
        if (err.status == 404) {
          this.router.navigate(['/vehicles']);
          return;
        }
      }
    );
  }

  delete() {
    if (confirm('Are you sure?')) {
      this.vehicleService.delete(this.vehicle.id).subscribe((x) => {
        this.router.navigate(['/vehicles']);
      });
    }
  }

  uploadPhoto($event) {
    this.photoService
      .upload(this.vehicleId, $event.target.files[0])
      .subscribe((photo) => this.photos.push(photo));
  }
}
