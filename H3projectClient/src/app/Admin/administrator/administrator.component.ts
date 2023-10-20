import { Component } from '@angular/core';

@Component({
  selector: 'app-administrator',
  templateUrl: './administrator.component.html',
  styleUrls: ['./administrator.component.css']
})
export class AdministratorComponent {
  isAdmin: boolean = false;

  toggleAdminPanel() {
    this.isAdmin = !this.isAdmin;
  }
}
