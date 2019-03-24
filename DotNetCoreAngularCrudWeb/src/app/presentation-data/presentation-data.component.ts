import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { PresentationsService } from 'app/services/presentations.service';
import { Presentations } from 'app/models/presentations';
@Component({
    selector: 'app-data',
    templateUrl: './presentation-data.component.html',
  })
  export class PresentationDataComponent{
      title = "";
      public results : Presentations[];
      constructor(private presentations:PresentationsService, private router:Router){
        let token = localStorage.getItem('TOKEN');
        if(!token){
          this.router.navigateByUrl('/');
        }else{
          this.presentations.getData()
          .subscribe(m => {
            if(m){
              this.results = m;
            }
          }, m => {console.error(m.error.error)});
        }
      }
      GetStatusByTitle(){
        this.presentations.getData(
          this.title
        ).subscribe(
          m => {
            if(m){
              this.results = m;
            }else{
              this.results = [];
            }
          }, error => console.error(error));
      }
  }