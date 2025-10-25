import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators, FormsModule } from '@angular/forms';
import { Quote, QuoteService } from '../../services/quoteService';
import { Router } from '@angular/router';

@Component({
    selector: 'app-quote-form',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule, FormsModule],
    templateUrl: './quote-form.html',
    styleUrls: ['./quote-form.scss']
})
export class QuoteFormComponent implements OnInit {
    addForm!: FormGroup;
    quotes: { id: number; author: string; quote: string; tags: string[] }[] = [];

    constructor(private fb: FormBuilder, private service: QuoteService, private router: Router) { }

    ngOnInit(): void {
        this.addForm = this.fb.group({
            author: ['', Validators.required],
            quoteText: ['', Validators.required],
            tags: ['']
        });

    }

    submitQuote(): void {
        if (this.addForm.invalid) return;

        const formValue = this.addForm.value;

        const newQuote: Quote = {
            author: formValue.author,
            quote: formValue.quoteText,  // map quoteText from form to quote
            tags: formValue.tags
                ? formValue.tags.split(',').map((t: string) => t.trim())
                : []
        };

        this.service.addQuote(newQuote).subscribe({
            next: (res) => {
                console.log('Quote saved', res);
                this.addForm.reset();
            },
            error: (err) => console.error('Error saving quote', err)
        });
    }
    

}
