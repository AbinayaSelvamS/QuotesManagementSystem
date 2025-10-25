import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { QuoteDto, QuoteService, RequestSearchDto } from '../../services/quoteService';

@Component({
    selector: 'app-bulk-add',
    standalone: true,
    imports: [CommonModule, ReactiveFormsModule],
    templateUrl: './bulk-add.html',
    styleUrls: ['./bulk-add.scss']
})
export class AddBulkComponent implements OnInit {
    bulkForm!: FormGroup;

    constructor(private fb: FormBuilder, private service: QuoteService) { }

    ngOnInit(): void {
        this.bulkForm = this.fb.group({
            quotes: this.fb.array([this.createQuote()])
        });
    }

    get quotes(): FormArray {
        return this.bulkForm.get('quotes') as FormArray;
    }

    createQuote(): FormGroup {
        return this.fb.group({
            author: ['', Validators.required],
            quote: ['', Validators.required],
            tags: ['']
        });
    }

    addQuote(): void {
        this.quotes.push(this.createQuote());
    }
    private resetForm(): void {
  this.bulkForm.setControl('quotes', this.fb.array([this.createQuote()]));
}


    saveBulk(): void {
        if (this.bulkForm.valid) {
            // Map form values to backend DTO
            const quotesArray = this.quotes.value.map((q: any) => ({
                author: q.author,
                quoteText: q.quote,  // must match backend
                tags: q.tags ? q.tags.split(',').map((t: string) => t.trim()) : []
            }));

            const dto = { quote: quotesArray }; // ✅ wrap in object

            console.log('Payload to send:', JSON.stringify(dto, null, 2));

            this.service.addBulk(dto).subscribe({
                next: () => {
                    alert('✅ Bulk quotes added successfully!');
                    this.resetForm();
                },
                error: (err) => console.error('❌ Error adding quotes:', err)
            });
        } else {
            alert('Please fill all required fields.');
        }
    }

}
