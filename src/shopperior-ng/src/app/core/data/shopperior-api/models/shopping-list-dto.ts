import { UserListPermissionDto } from "./user-list-permission-dto";

export interface ShoppingListDto {
  guid: string;
  name: string;
  permissions: UserListPermissionDto[];
}