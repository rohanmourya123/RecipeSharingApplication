import { Component, input } from '@angular/core';
import { FormControl, FormControlName, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { CommonModule } from '@angular/common'; // Needed if you use `ngIf`, etc.

@Component({
  selector: 'app-form-field',
  standalone: true,
  imports: [CommonModule, MatFormFieldModule, MatInputModule, MatIconModule, ReactiveFormsModule],
  templateUrl: './form-field.component.html',
  styleUrl: './form-field.component.css'
})
export class FormFieldComponent {

  noError:boolean = true;
  label = input<string>("");
  type = input<string>("text"); 
  placeholder = input<string>("");
  control = input<FormControl>();

  
}
