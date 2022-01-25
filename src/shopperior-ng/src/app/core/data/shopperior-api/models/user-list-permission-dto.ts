import { UserDto } from "./user-dto";

export interface UserListPermissionDto {
  user: UserDto;
  shoppingListGuid: string;
  permission: string;
}