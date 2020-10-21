import { HttpClientModule } from '@angular/common/http';
import { ErrorHandler, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { PhotoService } from './services/photo.service';
import { VehicleService } from './services/vehicle.service';
import { ToastyModule } from 'ng2-toasty';
import { AuthModule, AuthService } from '@auth0/auth0-angular';
import { AppComponent } from './app.component';
import { AppErrorHandler } from './app.error-handler';
import { HomeComponent } from './components/home/home.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { VehicleFormComponent } from './components/vehicle-form/vehicle-form.component';
import { VehicleListComponent } from './components/vehicle-list/vehicle-list.component';
import { PaginationComponent } from './components/shared/pagination/pagination.component';
import { ViewVehicleComponent } from './components/view-vehicle/view-vehicle.component';
import { AuthButtonComponent } from './auth-button/auth-button.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    VehicleFormComponent,
    VehicleListComponent,
    PaginationComponent,
    ViewVehicleComponent,
    AuthButtonComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ToastyModule.forRoot(),
    FontAwesomeModule,
    FormsModule,
    AuthModule.forRoot({
      domain: 'vega-ayers.us.auth0.com',
      clientId: 'OB7zRSnnKN2WXMz5v7BV5Ok7urVZa5WX',
    }),
    RouterModule.forRoot([
      { path: '', redirectTo: 'vehicles', pathMatch: 'full' },
      {
        path: 'vehicles/new',
        component: VehicleFormComponent,
        pathMatch: 'full',
      },
      {
        path: 'vehicles/edit/:id',
        component: VehicleFormComponent,
        pathMatch: 'full',
      },
      {
        path: 'vehicles/:id',
        component: ViewVehicleComponent,
        pathMatch: 'full',
      },
      {
        path: 'vehicles',
        component: VehicleListComponent,
        pathMatch: 'full',
      },
    ]),
  ],
  providers: [
    VehicleService,
    PhotoService,
    AuthService,
    { provide: ErrorHandler, useClass: AppErrorHandler },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
