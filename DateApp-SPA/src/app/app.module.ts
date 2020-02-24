import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { HttpClientModule } from '@angular/common/http';

// Componentes propios
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { ListsComponent } from './lists/lists.component';
import { MemberListComponent } from './members/member-list/member-list.component';

// Habilitar formularios y servicios
import { FormsModule } from '@angular/forms';
import { AuthService } from './_services/auth.service';
import { AlertifyService } from './_services/alertify.service';
import { UserService } from './_services/user.service';

// Interceptores
import { ErrorInterceptorProvider } from './_services/error.interceptor';

// Componentes ngx-bootstrap
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap';
import { MessagesComponent } from './messages/messages.component';
import { MemberCardComponent } from './members/member-card/member-card.component';

// Habilitar Angular routing
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { JwtModule } from '@auth0/angular-jwt';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';

export function tokenGetter() {
   return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      HomeComponent,
      RegisterComponent,
      ListsComponent,
      MemberListComponent,
      MessagesComponent,
      MemberCardComponent,
      MemberDetailComponent
   ],
   imports: [
      BrowserModule,
      BrowserAnimationsModule,
      HttpClientModule,
      FormsModule,
      BsDropdownModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      JwtModule.forRoot({
         config: {
            tokenGetter: tokenGetter,
            whitelistedDomains: ['localhost:5000'],
            blacklistedRoutes: ['localhost:5000/api/auth']
         }
      })
   ],
   providers: [
      AuthService,
      AlertifyService,
      UserService,
      ErrorInterceptorProvider
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
