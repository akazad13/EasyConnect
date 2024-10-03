import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { RolesModalComponent } from '../roles-modal/roles-modal.component';
import { User } from '../../models/user';
import { AdminService } from '../../services/admin.service';
import { AlertifyService } from '../../services/alertify.service';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-user-management',
  templateUrl: './user-management.component.html',
  styles: ``,
  imports: [NgFor],
  providers: [BsModalService],
  standalone: true,
})
export class UserManagementComponent implements OnInit {
  bsModalRef!: BsModalRef;
  users: User[] = [];

  constructor(
    private readonly adminService: AdminService,
    private readonly alertify: AlertifyService,
    private readonly modalService: BsModalService
  ) {}

  ngOnInit() {
    this.getUsersWithRoles();
  }

  getUsersWithRoles() {
    this.adminService.getUsersWithRoles().subscribe(
      (users: User[]) => {
        this.users = users;
      },
      (error: any) => {
        this.alertify.error(error);
      }
    );
  }

  editRolesModal(user: User) {
    const initialState = {
      user,
      roles: this.GetRolesArray(user),
    };
    this.bsModalRef = this.modalService.show(RolesModalComponent, { initialState });
    this.bsModalRef.content.updateSelectedRoles.subscribe((values: any[]) => {
      const rolesToUpdate = {
        roleName: [...values.filter((el) => el.checked === true).map((el) => el.name)],
      };

      if (rolesToUpdate) {
        this.adminService.updateUserRoles(user, rolesToUpdate).subscribe(
          () => {
            user.roles = [...rolesToUpdate.roleName];
          },
          (error: any) => {
            this.alertify.error(error);
          }
        );
      }
    });
  }

  private GetRolesArray(user: User) {
    const roles = [];
    const userRoles = user.roles;
    const availableRoles: any[] = [
      { name: 'Admin', value: 'Admin' },
      { name: 'Moderator', value: 'Moderator' },
      { name: 'Member', value: 'Member' },
    ];

    for (const availableRole of availableRoles) {
      let isMatch = false;
      for (const userRole of userRoles) {
        if (availableRole.name === userRole) {
          isMatch = true;
          availableRole.checked = true;
          roles.push(availableRole);
          break;
        }
      }

      if (!isMatch) {
        availableRole.checked = false;
        roles.push(availableRole);
      }
    }
    return roles;
  }
}
