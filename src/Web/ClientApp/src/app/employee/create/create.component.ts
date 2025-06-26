import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { EmployeeService } from '../employee.service';
import { Router } from '@angular/router';
import { ReactiveFormsModule, FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule,RouterModule],
  templateUrl: './create.component.html',
  styleUrl: './create.component.scss'
})
export class CreateComponent {
  form!: FormGroup;
        
    /*------------------------------------------
    --------------------------------------------
    Created constructor
    --------------------------------------------
    --------------------------------------------*/
    constructor(
      public employeeService: EmployeeService,
      private router: Router
    ) { }
        
    /**
     * Write code on Method
     *
     * @return response()
     */
    ngOnInit(): void {
      this.form = new FormGroup({
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
      this.employeeService.create(this.form.value).subscribe((res:any) => {
          console.log('employee created successfully!');
          this.router.navigateByUrl('employee/index');
      })
    }
}
