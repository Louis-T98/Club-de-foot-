import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/public/home-page/home-page.component').then(m => m.HomePageComponent)
  },
  {
    path: 'joueurs',
    loadComponent: () => import('./pages/public/joueurs-page/joueurs-page.component').then(m => m.JoueursPageComponent)
  },
  {
    path: 'equipes',
    loadComponent: () => import('./pages/public/equipes-page/equipes-page.component').then(m => m.EquipesPageComponent)
  },
  {
    path: 'matchs',
    loadComponent: () => import('./pages/public/matchs-page/matchs-page.component').then(m => m.MatchsPageComponent)
  },
  {
    path: 'stats',
    loadComponent: () => import('./pages/public/stats-page/stats-page.component').then(m => m.StatsPageComponent)
  },
  {
    path: 'login',
    loadComponent: () => import('./pages/public/login-page/login-page.component').then(m => m.LoginPageComponent)
  },
  {
    path: 'admin',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/admin/dashboard-page/dashboard-page.component').then(m => m.DashboardPageComponent)
  },
  {
    path: 'admin/joueurs',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/admin/admin-joueurs-page/admin-joueurs-page.component').then(m => m.AdminJoueursPageComponent)
  },
  {
    path: 'admin/equipes',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/admin/admin-equipes-page/admin-equipes-page.component').then(m => m.AdminEquipesPageComponent)
  },
  {
    path: 'admin/matchs',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/admin/admin-matchs-page/admin-matchs-page.component').then(m => m.AdminMatchsPageComponent)
  },
  {
    path: 'admin/entrainements',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/admin/admin-entrainements-page/admin-entrainements-page.component').then(m => m.AdminEntrainementsPageComponent)
  },
  {
    path: 'admin/blessures',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/admin/admin-blessures-page/admin-blessures-page.component').then(m => m.AdminBlessuresPageComponent)
  },
  {
    path: 'admin/staff',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/admin/admin-staff-page/admin-staff-page.component').then(m => m.AdminStaffPageComponent)
  },
  {
    path: 'admin/contrats',
    canActivate: [authGuard],
    loadComponent: () => import('./pages/admin/admin-contrats-page/admin-contrats-page.component').then(m => m.AdminContratsPageComponent)
  },
  {
    path: '**',
    redirectTo: ''
  }
];