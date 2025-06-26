import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { ModalModule } from 'ngx-bootstrap/modal';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { TodoComponent } from './todo/todo.component';

import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { AppRoutingModule } from './app-routing.module';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    TodoComponent,
    // ❌ REMOVE: IndexComponent, CreateComponent, EditComponent, ViewComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'todo', component: TodoComponent },
      // ✅ Add standalone components via `loadComponent`
      {
        path: 'employees',
        loadComponent: () =>
          import('./employee/index/index.component').then(
            (m) => m.IndexComponent
          ),
      },
      {
        path: 'employees/create',
        loadComponent: () =>
          import('./employee/create/create.component').then(
            (m) => m.CreateComponent
          ),
      },
      {
        path: 'employees/edit/:id',
        loadComponent: () =>
          import('./employee/edit/edit.component').then((m) => m.EditComponent),
      },
      {
        path: 'employees/view/:id',
        loadComponent: () =>
          import('./employee/view/view.component').then((m) => m.ViewComponent),
      },
    ]),
    BrowserAnimationsModule,
    AppRoutingModule,
    ModalModule.forRoot(),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
