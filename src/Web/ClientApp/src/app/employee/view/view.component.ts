import { Component, OnInit } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ActivatedRoute } from '@angular/router';
import { EmployeeService } from '../employee.service';
import { Employee } from '../employee';

@Component({
  selector: 'app-view',
  imports: [RouterModule],
  templateUrl: './view.component.html',
  styleUrl: './view.component.scss',
  standalone: true,
})
export class ViewComponent {
  id!: number;
  employee!: Employee;

  constructor(
    private route: ActivatedRoute,
    private employeeService: EmployeeService
  ) {}

  ngOnInit(): void {
    this.id = this.route.snapshot.params['empId'];
    this.employeeService.find(this.id).subscribe((response) => {
      this.employee = response.data;
    });
  }
}
