import { Routes } from '@angular/router';
import { QuoteList } from './components/quote-list/quote-list';
import { QuoteFormComponent } from './components/quote-form/quote-form';
import { AddBulkComponent } from './components/bulk-add/bulk-add';
import { DashboardComponent } from './components/dashboard/dashboard';

export const routes: Routes = [
{ path: 'dashboard', component: DashboardComponent },
  { path: 'quotes', component: QuoteList },
  {path: 'add',  component: QuoteFormComponent },
  { path: 'bulk', component: AddBulkComponent },
  { path: '', redirectTo: '/dashboard', pathMatch: 'full' },
];
