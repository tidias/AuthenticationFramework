import {Component, NgZone, OnInit} from '@angular/core';
import {UtilsService} from '../shared/utils.service';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent implements OnInit {
  apiResponse;

  constructor(public utils: UtilsService) {
  }

  ngOnInit(): void {
    this.getValues();
  }


  async getValues() {
    const response = await this.utils.sendGetRequest(this.utils.SERVER_URL + 'login');
    this.apiResponse = JSON.stringify(response.info);
  }
}
