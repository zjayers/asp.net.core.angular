import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ISaveVehicle } from '../types';

@Injectable({
  providedIn: 'root',
})
export class VehicleService {
  private readonly vehiclesEndpoint = '/api/vehicles';

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

  getVehicles(filter) {
    return this.http.get(
      this.vehiclesEndpoint + '?' + this.toQueryString(filter)
    );
  }

  getVehicle(id: number | string) {
    return this.http.get(this.vehiclesEndpoint + `/${id}`);
  }

  create(vehicle: ISaveVehicle) {
    return this.http.post(this.vehiclesEndpoint, vehicle);
  }

  update(vehicle: ISaveVehicle) {
    return this.http.put(this.vehiclesEndpoint + '/' + vehicle.id, vehicle);
  }

  delete(id) {
    return this.http.delete(this.vehiclesEndpoint + `/${id}`);
  }

  private toQueryString(filter): string {
    const parts = [];
    for (const property in filter) {
      if (filter.hasOwnProperty(property)) {
        const value = filter[property];
        if (value != null) {
          parts.push(
            encodeURIComponent(property) + '=' + encodeURIComponent(value)
          );
        }
      }
    }
    return parts.join('&');
  }
}
