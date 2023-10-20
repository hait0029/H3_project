import { Component } from '@angular/core';

@Component({
  selector: 'app-testbool',
  templateUrl: './testbool.component.html',
  styleUrls: ['./testbool.component.css']
})
export class TestboolComponent {
  showAdminPanel: boolean = true;

  toggleAdminPanel() {
    this.showAdminPanel = !this.showAdminPanel;
}
}
