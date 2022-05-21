import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-trace-ip',
  templateUrl: './trace-ip.component.html'
})

export class TraceIpComponent {

  private http: HttpClient;
  private baseUrl: string;
  private completeUrl: string;
  public country: Country;
 
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.completeUrl = this.baseUrl + 'iptracker';
  }

  checkIp(ipNumber: string) {
    if (ipNumber) {
      const url = `${this.completeUrl}/${ipNumber}`;
      this.http.get<Country>(url).subscribe(result => {
        this.country = result;
      }, error => console.error(error));
    }
  }
}

interface Country {
  country: string;
  isoCode: string;
  timezone: string;
  currency: string;
  distanceToBA: string;
}
