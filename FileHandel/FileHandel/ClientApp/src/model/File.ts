export class File {
  constructor(
    public fileID: number,
    public fileName: string,
    public fileType: string,
    public fileSize: number,
    public fileSizeFormat: string,
    public aouther: string,
    public dateAouther: Date,
    public isEncoded: boolean
  ) {}
}
