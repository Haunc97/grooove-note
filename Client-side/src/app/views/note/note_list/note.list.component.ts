import { Component, OnInit, ViewChild } from '@angular/core';
import {NoteService} from '../../../services/note.service';
import {Note} from '../../../models/note.model';
import {Router} from '@angular/router'
import {ModalDirective} from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-notes',
  templateUrl: './note.list.component.html',
  styleUrls: ['./../note.style.css']
})
export class NoteListComponent implements OnInit {
  noteList : Array<Note>;
  public loading = false;
  public currentNote: Note = new Note();
  noteFilter: any = { title: '' };
  @ViewChild('dangerModal', {static: false}) public dangerModal: ModalDirective;
  constructor(private noteService:NoteService, private router:Router, private toastr : ToastrService){
  }

  ngOnInit() {
    this.GetAllNote();
  }
  GetAllNote(){
    this.loading = true;
    this.noteService.getNoteList().subscribe(
      data=> {
        this.noteList = data;
        this.loading = false;
      },
      err=>{
        console.log(err);
        this.loading = false;
      })
  }
  public setCurrentNote(currentNote : Note){
    this.currentNote = currentNote;
  }
  onDeleteNote(){
    this.noteService.deleteNote(this.currentNote.id).subscribe(
      success=>{
        this.toastr.success('Delete '+this.currentNote.title + ' successfully.','Notification');
        this.GetAllNote();
      },
      error =>this.toastr.error('Delete '+this.currentNote.title + ' fail.',"Notification")
    )
  }
  updateStatus(item:Note){
    item.finished = !item.finished;
    this.noteService.saveNote(item).subscribe(data=>{
      this.toastr.success('Updated successfully.','Notification');
    },err=>{
      console.log(err);
    });
  }
  goToEditPage(item : Note){
    this.router.navigate(['/notes/create',{item:btoa(item.id.toString())}]);
  }
}
