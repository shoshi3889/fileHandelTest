import { Component, OnInit } from '@angular/core';
import { File } from '../../model/File';
import { FileService } from '../../manager/file.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.css'],
})
export class ListComponent implements OnInit {
  files: File[] = [];
  sumSizeInGB = '';
  constructor(private fileService: FileService) {}

  ngOnInit() {
    this.getFiles().subscribe((files) => {
      this.files = files;
      this.sumSizeInGB = files
        .reduce(
          (sum, item) =>
            sum + this.calculationSizeInGB(item.fileSize, item.fileSizeFormat),
          0
        )
        .toFixed(3);
    });
  }

  getFiles() {
    return this.fileService.getFiles();
  }
  splitFile(id: number): void {
    this.fileService.splitFile(id).subscribe(
      (result) => {
        this.files = result;
      },
      (error) => {}
    );
  }
  calculationSizeInGB(size: number, format: string) {
    var sizeFormat = ['Byte', 'KB', 'MB', 'GB'];
    const sizeFormatIndex = sizeFormat.findIndex((size) => size == format);
    sizeFormat.splice(sizeFormatIndex + 1).forEach(() => (size /= 1024));
    return size;
  }
  isCanSplit(fileSize: number, fileSizeFormat: string) {
    return this.calculationSizeInGB(fileSize, fileSizeFormat) > 1;
  }
  shareFiels() {}
}
