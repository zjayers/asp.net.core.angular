import { Component, OnInit } from '@angular/core';
import { faSortDown, faSortUp } from '@fortawesome/free-solid-svg-icons';
import { VehicleService } from '../../services/vehicle.service';
import { IKeyValuePair, IQuery, IQueryResult } from '../../types';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css'],
})
export class VehicleListComponent implements OnInit {
  private readonly PAGE_SIZE = 10;
  private readonly EMPTY_QUERY = {
    isSortAscending: false,
    makeId: '',
    modelId: '',
    sortBy: '',
    page: 1,
    pageSize: this.PAGE_SIZE,
  };

  queryResult: IQueryResult = { items: [], totalItems: 0 };
  makes: IKeyValuePair[];
  models: IKeyValuePair[];
  query: IQuery = this.EMPTY_QUERY;
  columns = [
    { title: 'Id' },
    { title: 'Contact Name', key: 'contactName', isSortable: true },
    { title: 'Make', key: 'make', isSortable: true },
    { title: 'Model', key: 'model', isSortable: true },
  ];

  // Icons
  faSortUp = faSortUp;
  faSortDown = faSortDown;

  constructor(private vehicleService: VehicleService) {}

  ngOnInit() {
    this.populateVehicles();
    this.vehicleService
      .getMakes()
      .subscribe((makes) => (this.makes = makes as IKeyValuePair[]));

    this.populateModels();
  }

  private populateVehicles() {
    this.vehicleService
      .getVehicles(this.query)
      .subscribe((result: IQueryResult) => {
        console.log(result);
        return (this.queryResult = result);
      });
  }

  private populateModels() {
    if (this.query.makeId) {
      this.vehicleService
        .getOneMake(this.query.makeId)
        .subscribe((m: any) => (this.models = m.models));
    }
  }

  onFilterChange() {
    this.query = this.EMPTY_QUERY;
    this.populateModels();
    this.populateVehicles();
    delete this.query.modelId;
  }

  resetFilters() {
    this.query = this.EMPTY_QUERY;
    this.onFilterChange();
  }

  sortBy(columnName: string) {
    if (this.query.sortBy == columnName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = columnName;
      this.query.isSortAscending = true;
    }

    this.populateVehicles();
  }

  onPageChange(pageNumber: any) {
    this.query.page = pageNumber;
    this.populateVehicles();
  }
}
