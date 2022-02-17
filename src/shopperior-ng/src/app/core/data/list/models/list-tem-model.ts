export class ListItemModel {
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
  hasPurchased: boolean;
  purchasedTime?: Date;
}