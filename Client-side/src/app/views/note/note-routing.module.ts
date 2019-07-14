import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { NoteListComponent } from './note_list/note.list.component';
import {NoteCreateComponent} from './note_save/note.save.component';
import { AuthGuard } from '../../auth.guard';

const routes: Routes = [
  {
    path: '',
    data: {
      title: 'Note'
    },
    children: [
      {
        path: '',
        redirectTo: 'list'
      },
      {
        path: 'list',
        component: NoteListComponent,
        data: {
          title: 'List'
        }
      },
      {
        path: 'create',
        component: NoteCreateComponent,
        data: {
          title: 'Create'
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NoteRoutingModule {}
