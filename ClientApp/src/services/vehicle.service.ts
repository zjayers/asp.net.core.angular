import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class VehicleService {
  constructor(private http: HttpClient) {}

  getMakes() {
    return this.http.get('api/makes');
  }

  getFeatures() {
    return this.http.get('api/features');
  }

  getOneMake(id: number | string) {
    return this.http.get(`/api/makes/${id}`);
  }

  getVehicle(id: number | string) {
    return this.http.get(`/api/vehicles/${id}`);
  }

  create(vehicle) {
    return this.http.post('/api/vehicles', vehicle);
  }

  delete(id) {
    return this.http.delete(`/api/vehicles/${id}`);
  }
}
