export interface ListItemDto {
  guid: string;
  shoppingListGuid: string;
  name: string;
  brand: string;
  comment: string;
  quantity: number;
  measurement: string;
  unitPrice: number;
  totalPrice: number;
  isInCart: boolean;
  enteredCartTime?: Date;
  hasPurchased: boolean;
  purchasedTime?: Date;
}