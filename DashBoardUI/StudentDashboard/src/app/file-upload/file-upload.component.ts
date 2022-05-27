import { Component, OnInit, Output, EventEmitter } from '@angular/core';

import { responseStatus } from '../model/responseDetails';
import { StudentGradeData } from '../model/StudentGradeDataModel';
import { StudentDataService } from '../service/student-data.service';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent implements OnInit {

  constructor(private studentDataService:StudentDataService) { }


  ngOnInit(): void {
  }

  @Output() onDataParsed = new EventEmitter<any>();

  file:File;
  fileName:string = "";

  title = 'StudentDashboard';

  onUpload(event:any){
  this.file = event.target.files[0]
  this.fileName = this.file.name;

  this.studentDataService.postStuddentData(this.file).subscribe(response=>{
    if(response.responseStatus === responseStatus.success){
      this.onDataParsed.emit(response.responseData);
    }
   
  },(error)=>{
    console.log(error)
  },()=>{
    event.target.files = null;
    event.target.value = '';
  });
  }

}
