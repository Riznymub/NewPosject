import { Component } from '@angular/core';

import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { EmployeeService } from '../employee.service';
import { Employee } from '../employee';

@Component({
  selector: 'app-index',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './index.component.html',
  styleUrl: './index.component.scss'
})
export class IndexComponent {
   employees: Employee[] = [];
      
  pageNumber = 1;
  pageSize = 10;
  totalPages = 0;
  totalCount = 0;
  constructor(public employeeService: EmployeeService) { }
      


 ngOnInit(): void {
    this.getEmployees();
  }

  getEmployees(): void {
    this.employeeService.getAll(this.pageNumber, this.pageSize).subscribe(response => {
      this.employees = response.data.items;
      this.totalCount = response.data.totalCount;
      this.totalPages = Math.ceil(this.totalCount / this.pageSize);
    });
  }

  nextPage() {
    if (this.pageNumber < this.totalPages) {
      this.pageNumber++;
      this.getEmployees();
    }
  }

  prevPage() {
    if (this.pageNumber > 1) {
      this.pageNumber--;
      this.getEmployees();
    }
  }

  goToPage(n: number) {
    this.pageNumber = n;
    this.getEmployees();
  }
      
 
  deleteEmployee(id:number){
    this.employeeService.delete(id).subscribe(res => {
         this.employees = this.employees.filter(item => item.id !== id);
         console.log('employee deleted successfully!');
    })
  }
}
