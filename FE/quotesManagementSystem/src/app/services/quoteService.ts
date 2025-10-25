import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Quote {
  id?: number;
  author: string;
  quote: string;
  tags?: string[];
}
// src/app/models/quote.model.ts

export interface QuoteDto {
  author: string;
  quoteText: string;
  tags: string[];
}

export interface RequestSearchDto {
  quote: QuoteDto[];
}


@Injectable({ providedIn: 'root' })
export class QuoteService {
  private baseUrl = 'https://localhost:7114/api/Quotes';
  // https://localhost:7114/api/Quotes/post
private getallUrl = `${this.baseUrl}/Getall`;
private postUrl = `${this.baseUrl}/post`;

  constructor(private http: HttpClient) {}

  getAll(): Observable<Quote[]> {
    return this.http.get<Quote[]>(this.getallUrl);
  }

  addQuote(q: Quote): Observable<Quote> {
    return this.http.post<Quote>(this.postUrl, q);
  }

  addBulk(dto: RequestSearchDto): Observable<any> {
    return this.http.post(`${this.baseUrl}/add-multiple`, dto);
  }
  deleteQuote(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/DeleteBy/${id}`);
  }
  updateQuote(id: number, q: Quote): Observable<Quote> {
    return this.http.put<Quote>(`${this.baseUrl}/PostBy/${id}`, q);
  }

}
