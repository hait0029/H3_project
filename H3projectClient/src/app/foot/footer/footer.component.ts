import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent {
  // When the user clicks on the button, scroll to the top of the document
  goToTop() {
    document.body.scrollTop = 0; // For Safari
    document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
  }

  goToWeb() {
    window.open('https://www.youtube.com/watch?v=dQw4w9WgXcQ');
  }
}
