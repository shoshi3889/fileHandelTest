import { Component, Input } from '@angular/core';
import { File } from 'src/model/File';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FileService } from 'src/manager/file.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-file-component',
  templateUrl: './file.component.html',
  styleUrls: ['./file.component.css'],
})
export class FileComponent {
  file: File | undefined;

  fileForm = new FormGroup({
    fileID: new FormControl(0, [Validators.required]),
    fileName: new FormControl('', [
      Validators.required,
      Validators.pattern('[a-zA-Zא-ת_0-9 ]*'),
    ]),
    aouther: new FormControl('', [Validators.pattern('[a-zA-Zא-ת ]*')]),
    fileType: new FormControl('', [Validators.required]),
    fileSize: new FormControl(0, [
      Validators.required,
      Validators.pattern('^[-]?[0-9]+(?:.[0-9]+)?$'),
      Validators.maxLength(5),
    ]),
    fileSizeFormat: new FormControl('KB', [Validators.required]),
    isEncoded: new FormControl(false),
  });
  constructor(
    private route: ActivatedRoute,
    private fileService: FileService,
    private location: Location
  ) {}

  ngOnInit(): void {
    this.getFile();
  }

  public saveFile() {
    if (this.fileForm.valid) {
      var file = this.fileForm.value;
      this.fileService.saveFile(file as File).subscribe(
        (result) => {
          this.location.back();
        },
        (error) => {}
      );
    }
  }

  getFile(): void {
    const id = Number(this.route.snapshot.paramMap.get('id')) || 0;
    const file = this.fileService.getFile(id);
    this.fileForm.setValue({
      fileID: id,
      fileName: file?.fileName || '',
      fileType: file?.fileType || '',
      fileSize: file?.fileSize || 0,
      aouther: file?.aouther || '',
      fileSizeFormat: file?.fileSizeFormat || '',
      isEncoded: file?.isEncoded || false,
    });
  }

  get fielFormControl() {
    return this.fileForm.controls;
  }
}
