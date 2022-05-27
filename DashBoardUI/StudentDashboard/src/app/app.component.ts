import { Component, OnInit } from '@angular/core';
import { StudentGradeData } from './model/StudentGradeDataModel';
import { StudentDataService } from './service/student-data.service';

export interface Tile {
  color: string;
  cols: number;
  rows: number;
  text: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})


export class AppComponent implements OnInit {
  ngOnInit(): void {
    // let localdata = localStorage.getItem('dataSource');
    // if(localdata){
    //   this.data = JSON.parse(localdata);
    //   this.data.grades.forEach(x=>{
    //     x.subjects.forEach(y=>{
    //       this.viewModel.push({
    //         'gradeNumner':x.gradeNumber,
    //         'subjectId':y.subjectId,
    //         'subjectName':y.subjectName
    //       })
    //     })
    //   })
  
    //   this.sliderMax = this.viewModel.length-1;
    // }
  }
  constructor(private studentDataService:StudentDataService) {

  }

  tiles: Tile[] = [
    { text: 'One', cols: 3, rows: 1, color: 'lightblue' },
    { text: 'Two', cols: 1, rows: 2, color: 'lightgreen' },
    { text: 'Three', cols: 1, rows: 1, color: 'lightpink' },
    { text: 'Four', cols: 2, rows: 1, color: '#DDBDF1' },
  ];



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

    //localStorage.setItem('dataSource',JSON.stringify(this.data));

    console.log(eventData);
  }



}
