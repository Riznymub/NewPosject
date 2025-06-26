import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { EmployeeService } from '../employee.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from '../employee';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit',
    standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './edit.component.html',
  styleUrl: './edit.component.scss'
})
export class EditComponent {
  id!: number;
  employee!: Employee;
  form!: FormGroup;
      
  /*------------------------------------------
  --------------------------------------------
  Created constructor
  --------------------------------------------
  --------------------------------------------*/
  constructor(
    public employeeService: EmployeeService,
    private route: ActivatedRoute,
    private router: Router
  ) { }
      
  /**
   * Write code on Method
   *
   * @return response()
   */
  ngOnInit(): void {
    this.id = this.route.snapshot.params['empId'];
    this.employeeService.find(this.id).subscribe((response)=>{
      this.employee = response.data;
    }); 
        
    this.form = new FormGroup({
        id: new FormControl(this.id),
        name: new FormControl('', [Validators.required,Validators.maxLength(150)]),
        age: new FormControl('', [Validators.required, Validators.min(12),Validators.max(100)]),
        department: new FormControl('', [Validators.required,Validators.maxLength(200)])
    });
  }
      
  /**
   * Write code on Method
   *
   * @return response()
   */
  get f(){
    return this.form.controls;
  }
      
  /**
   * Write code on Method
   *
   * @return response()
   */
  submit(){
    console.log(this.form.value);
    this.employeeService.update(this.id, this.form.value).subscribe((res:any) => {
         console.log('Employee updated successfully!');
         this.router.navigateByUrl('employee/index');
    })
  }

  resetForm() {
    if (this.employee) {
      this.form.patchValue(this.employee);
      this.form.markAsPristine(); 
      this.form.markAsUntouched(); 
    } else {
      this.form.reset();
    }
  }
}
