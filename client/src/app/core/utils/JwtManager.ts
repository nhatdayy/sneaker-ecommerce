import { secretKeyToken } from '../constants/secret';
import * as CryptoJS from 'crypto-js';
export class JwtManager {
  encryptToken(token: string): string {
    const encryptToken = CryptoJS.AES.encrypt(token, secretKeyToken).toString();
    return encryptToken;
  }
  decryptToken(token: string): string {
    const bytes = CryptoJS.AES.decrypt(token, secretKeyToken);
    const decryptedToken = bytes.toString(CryptoJS.enc.Utf8);
    return decryptedToken;
  }
  getToken(): string {
    const token = localStorage.getItem('token') ?? '';
    return token ? this.decryptToken(token) : '';
  }
  setToken(token: string): void {
    const tokenEncrypt = this.encryptToken(token);
    localStorage.setItem('token', tokenEncrypt);
  }
}
