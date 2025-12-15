import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { User } from '../../models/user'; // import your User model

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(private auth: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean {
    const expectedRole = route.data['role'];

    // Typecast getUser() to any or User | null
    const user = this.auth.getUser() as any;  // <--- FIX

    if (!user || !user.role) {   // now TS will not complain
      this.router.navigate(['/login']);
      return false;
    }

    if (Array.isArray(expectedRole) && expectedRole.includes(user.role)) {
      return true;
    }

    if (user.role === expectedRole) {
      return true;
    }

    this.router.navigate(['/unauthorized']);
    return false;
  }
}
