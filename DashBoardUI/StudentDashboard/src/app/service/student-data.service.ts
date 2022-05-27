import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { studentDataConfig, scoreDataConfig } from "../../environments/environment";
import { ResponseDetails } from '../model/responseDetails';
import { StudentGradeData } from '../model/StudentGradeDataModel';

@Injectable({
  providedIn: 'root'
})
export class StudentDataService {

  constructor(private http:HttpClient) { }

  postStuddentData(file:File):Observable<ResponseDetails<StudentGradeData>>{
    const formData = new FormData();
    let url = studentDataConfig.studentServiceBaseUrl+studentDataConfig.postStudentConfigEndPoint
    formData.append('file',file,file.name);
    return this.http.post<ResponseDetails<StudentGradeData>>(url,formData);
  }


  postStudentScoreData(subjectIds:number[]):Observable<ResponseDetails<any>>{
    let url = scoreDataConfig.scoreServiceBaseUrl+scoreDataConfig.postScoreConfigEndPoint;
    return this.http.post<ResponseDetails<any>>(url,subjectIds);
  }
}
