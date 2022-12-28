import { Component, Inject } from '@angular/core';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';

import { File } from '../model/File';

@Injectable({
  providedIn: 'root',
})
export class FileService {
  public FILES: File[] = [];
  baseUrl: string = '';

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  getFiles() {
    return this.http.get<File[]>('/files/Get').pipe(
      tap((data) => {
        this.FILES = data;
      })
    );
  }
  getFile = (id: number) => this.FILES.find((file) => file.fileID == id);

  saveFile(file: File | undefined) {
    return this.http.post<File[]>('/files/SaveFile', file).pipe();
  }
  splitFile(id: number) {
    return this.http.post<File[]>('/files/SplitFile', id).pipe();
  }
}
