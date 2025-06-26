import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RouterModule, Routes } from '@angular/router';
import { IndexComponent } from './employee/index/index.component';
import { ViewComponent } from './employee/view/view.component';
import { CreateComponent } from './employee/create/create.component';
import { EditComponent } from './employee/edit/edit.component';

const routes: Routes = [
      { path: 'employee', redirectTo: 'employee/index', pathMatch: 'full'},
      { path: 'employee/index', component: IndexComponent },
      { path: 'employee/:empId/view', component: ViewComponent },
      { path: 'employee/create', component: CreateComponent },
      { path: 'employee/:empId/edit', component: EditComponent }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
