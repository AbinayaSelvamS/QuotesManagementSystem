import { Component, OnInit } from '@angular/core';
import { QuoteService } from '../../services/quoteService';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  imports: [CommonModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class DashboardComponent implements OnInit {
  totalQuotes = 0;
  topAuthor = '';
  topTag = '';

  constructor(private quoteService: QuoteService) {}

  ngOnInit(): void {
    this.quoteService.getAll().subscribe((quotes) => {
      this.totalQuotes = quotes.length;

      // simple stats
      const authorCount: Record<string, number> = {};
      const tagCount: Record<string, number> = {};

      quotes.forEach(q => {
        if (q.author) authorCount[q.author] = (authorCount[q.author] || 0) + 1;
        if (q.tags) q.tags.forEach(tag => {
          tagCount[tag] = (tagCount[tag] || 0) + 1;
        });
      });

      this.topAuthor = Object.entries(authorCount).sort((a, b) => b[1] - a[1])[0]?.[0] || 'N/A';
      this.topTag = Object.entries(tagCount).sort((a, b) => b[1] - a[1])[0]?.[0] || 'N/A';
    });
  }
}