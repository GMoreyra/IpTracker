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
  public averageKms: string;
  public country: Country;
  public statistics: Statistic[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.http = http;
    this.baseUrl = baseUrl;
    this.completeUrl = this.baseUrl + 'iptracker';
  }

  checkIp(ipAddress: string) {
    this.setNull();
    if (ipAddress) {
      const url = `${this.completeUrl}/${ipAddress}`;
      this.http.get<Country>(url).subscribe(result => {
        this.country = result;
      }, error => console.error(error));
    }
  }

  checkStatistic() {
    this.setNull();
    const url = `${this.completeUrl}/statistic`;
    this.http.get<Statistic[]>(url).subscribe(result => {
      this.statistics = result;
    }, error => console.error(error));
  }

  checkAverage() {
    this.setNull();
    const url = `${this.completeUrl}/average`;
    this.http.get(url, { responseType: 'text' }).subscribe(result => {
      this.averageKms = result.toString();
    }, error => console.error(error));
  }

  setNull() {
    this.country = null;
    this.statistics = null;
    this.averageKms = null;
  }
}

interface Country {
  country: string;
  isoCode: string;
  timezone: string;
  currency: string;
  distanceToBA: string;
}

interface Statistic {
  countryName: string;
  distanceToBaInKms: number;
  InvocationCounter: number;
}
