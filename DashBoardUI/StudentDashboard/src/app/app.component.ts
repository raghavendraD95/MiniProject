import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ResponseDetails } from './model/responseDetails';
import { StudentGradeData } from './model/StudentGradeDataModel';
import { StudentDataService } from './service/student-data.service';
import { WebSocketConnectionService } from './service/web-socket-connection.service';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent implements OnInit {
  ngOnInit(): void {
    
  }
  constructor(private studentDataService:StudentDataService,private socketService: WebSocketConnectionService,
    private snackBar:MatSnackBar) {
    socketService.connect()
    .subscribe(response=>{
      if(response){
      console.log("calling the special method");        
      var socketEventData = response as ResponseDetails<any>;
      var studentData = socketEventData.responseData
      this.snackBar.open("Student data has been updated.","Close",{
        duration: 4000
      })
      this.createGradeView(studentData);
      }
    });
  }

  data: StudentGradeData;
  viewModel:any[] = [];
  sliderMin:number = 0;
  sliderMax:number;
  sliderValue:number;

  columnsToDisplay:string[] = ['gradeNumber', 'subjectName', 'scores'];

  selectedColor:string = 'cyan';

  dataSource: any[] = [];

  changeValue(data:any)
  {
    
    let index = data.value;

    let selectedGradeData:number[];

    if(index-1<=0){
      
      selectedGradeData = [0,1,2]

    }
    else if(index+1>=this.viewModel.length){

      selectedGradeData = [index,index-1,index-2]

    }
    else{
      selectedGradeData = [index-1, index, index+1];
    }

    this.getScoreData(selectedGradeData);
  }

  getScoreData(indexes:number[]){

    let selectedGradeData:any[] = [];

    

    indexes.forEach(x=>{
      selectedGradeData.push({
        gradeNumber:this.viewModel[x].gradeNumner,
        subjectId:this.viewModel[x].subjectId,
        subjectName:this.viewModel[x].subjectName,
        scores:0
      })
    })

    if (selectedGradeData && selectedGradeData.length > 0) {
      this.studentDataService.postStudentScoreData(selectedGradeData.map(x => { return x.subjectId }))
        .subscribe(response => {
          let scoreData:any[] = response.responseData;
          this.dataSource.map(x=>{
            x.scores = scoreData.filter(y=>x.subjectId === y.subjectId)[0].score;
          });
        });
    }

    this.dataSource = selectedGradeData;

    console.log(selectedGradeData);

  }

  createGradeView(eventData: any) {
    this.data = eventData;


    this.viewModel = [];
    this.data.grades.forEach(x=>{
      x.subjects.forEach(y=>{
        this.viewModel.push({
          'gradeNumner':x.gradeNumber,
          'subjectId':y.subjectId,
          'subjectName':y.subjectName
        })
      })
    })

    this.sliderMax = this.viewModel.length-1;

    console.log(eventData);

  }



}
