import { AccessRights } from "./project";

export interface UserAccessRights {
  fullname: string,
  email: string,
  accessRights: AccessRights
}
