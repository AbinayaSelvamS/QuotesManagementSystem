import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Quote, QuoteDto, QuoteService } from '../../services/quoteService';

@Component({
  selector: 'app-quote-list',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './quote-list.html',
  styleUrls: ['./quote-list.scss']
})
export class QuoteList implements OnInit {
  quotes: Quote[] = [];
  searchForm!: FormGroup;
  editForm!: FormGroup;
  editingQuoteId: number | null = null;

  constructor(private fb: FormBuilder, private service: QuoteService) {}

  ngOnInit(): void {
    this.searchForm = this.fb.group({
      author: [''],
      quote: [''],
      tags: ['']
    });

    // ✅ Initialize editForm here
    this.editForm = this.fb.group({
      author: [''],
      quoteText: [''],
      tags: ['']
    });

    this.getUserData();
  }

  // ✅ Fetch quotes from backend
  getUserData(): void {
    this.service.getAll().subscribe({
      next: (data) => {
        this.quotes = data.map(q => ({
          id: q.id ?? 0,
          author: q.author,
          quote: q.quote,
          tags: q.tags ? q.tags.map((t) => t.trim()) : []
        }));
      },
      error: (err) => console.error('Error fetching quotes:', err)
    });
  }

  // ✅ Filter by search
  loadQuotes(): void {
    const { author, quote, tags } = this.searchForm.value;
    const searchAuthor = author?.toLowerCase() || '';
    const searchQuote = quote?.toLowerCase() || '';
    const searchTags = tags?.toLowerCase() || '';

    this.quotes = this.quotes.filter(x => {
      return (
        (!searchAuthor || x.author.toLowerCase().includes(searchAuthor)) &&
        (!searchQuote || x.quote.toLowerCase().includes(searchQuote)) &&
        (!searchTags || x.tags?.some(t => t.toLowerCase().includes(searchTags)))
      );
    });
  }

  // ✅ Enter edit mode
  editQuote(q: Quote) {
    this.editingQuoteId = q.id!;
    this.editForm.patchValue({
      author: q.author,
      quoteText: q.quote,
      tags: q.tags ? q.tags.join(', ') : ''
    });
  }

  // ✅ Save the updated quote
  saveQuote() {
    if (!this.editForm.valid || this.editingQuoteId === null) return;

    const updated: Quote = {
      author: this.editForm.value.author,
      quote: this.editForm.value.quoteText,
      tags: this.editForm.value.tags
        ? this.editForm.value.tags.split(',').map((t: string) => t.trim())
        : []
    };

    this.service.updateQuote(this.editingQuoteId, updated).subscribe({
      next: () => {
        console.log('✅ Quote updated successfully!');
        this.editingQuoteId = null;
        this.editForm.reset();
        this.getUserData(); // refresh list after update
      },
      error: (err) => console.error('Error updating quote:', err)
    });
  }

  // ✅ Cancel editing
  cancelEdit() {
    this.editingQuoteId = null;
    this.editForm.reset();
  }

  //  Delete quote
  deleteQuote(id: number) {
    if (confirm('Are you sure you want to delete this quote?')) {
      this.service.deleteQuote(id).subscribe(() => {
        console.log('Quote deleted successfully!');
        this.getUserData(); // refresh after delete
      });
    }
  }
}
