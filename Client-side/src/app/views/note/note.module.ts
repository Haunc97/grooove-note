// Angular
import { CommonModule } from '@angular/common';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { ModalModule } from 'ngx-bootstrap/modal';
import { NgxLoadingModule } from 'ngx-loading';
import { FilterPipeModule } from 'ngx-filter-pipe';
// Components
import {NoteListComponent} from './note_list/note.list.component'
import {NoteCreateComponent} from './note_save/note.save.component'
// Components Routing
import { NoteRoutingModule } from './note-routing.module';
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    NoteRoutingModule,
    CKEditorModule,
    ReactiveFormsModule,
    FilterPipeModule,
    ModalModule.forRoot(),
    NgxLoadingModule.forRoot({})
  ],
  declarations: [
    NoteListComponent,
    NoteCreateComponent
  ]
})
export class NoteModule { }
