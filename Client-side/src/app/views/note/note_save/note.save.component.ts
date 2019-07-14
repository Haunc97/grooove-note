import { Component, OnInit } from '@angular/core';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router'
// Models
import { Note } from '../../../models/note.model';
import { Notebook } from '../../../models/notebook.model';
// Services
import { NotebookService } from '../../../services/notebook.service';
import { NoteService } from '../../../services/note.service';
import { ToastrService } from 'ngx-toastr';

export interface Food {
  value: string;
  viewValue: string;
}
@Component({
  selector: 'app-note.create',
  templateUrl: './note.save.component.html',
  styleUrls: ['./../note.style.css']
})

export class NoteCreateComponent implements OnInit {
  public Editor = ClassicEditor;
  public note: Note = new Note();
  public notebookList: Array<Notebook> = new Array<Notebook>();
  AddNoteForm: FormGroup ;
  noteIdForEdit: number;
  loading:boolean=false;
  isLoadForm: boolean = false;
  constructor(private act_router: ActivatedRoute, private router: Router, private frmBuilder: FormBuilder, private toastr: ToastrService, private notebookService: NotebookService, private noteService: NoteService) { }

  async ngOnInit() {
    await this.act_router.paramMap.subscribe(params => {
      this.noteIdForEdit = Number.parseInt(atob(params.get('item')));
      if (this.noteIdForEdit) {
        this.getNoteToEdit();
      } else {
        this.note.notebookId = 0;
        this.initForm();
      }
    })
    this.loading = false;
    this.getNotebookList();
  }
  async initForm() {
    this.loading = true;
    this.AddNoteForm = await this.frmBuilder.group({
      title: [this.note.title, Validators.compose([Validators.required,Validators.maxLength(50)])],
      description: [this.note.description, Validators.compose([Validators.required])],
      notebookId: [this.note.notebookId, Validators.compose([Validators.pattern('^[1-9]\d*$')])],
      finished: [this.note.finished=false]
    });
    this.isLoadForm = true;
    this.loading = false;
  }
  getNoteToEdit() {
    this.noteService.getSingleNote(this.noteIdForEdit).subscribe((data: Note) => {
      this.note = data;
      this.initForm();
    }, err => {
      console.log(err);
    });
  }
  getNotebookList() {
    this.notebookService.getNotebookList().subscribe(
      data => {
        this.notebookList = data;
      },
      err => {
        console.log(err);
      }
    );
  }
  onAddNote() {
    this.noteService.saveNote(this.note).subscribe(
      (data: Note) => {
        this.router.navigate(['/notes/list']);
        this.toastr.success("Note " + data.title + " saved successfully","Notification",);
      },
      err => {
        this.toastr.error("Saving note failed.","Notification")
        console.log(err);
      }
    );
  }
}
