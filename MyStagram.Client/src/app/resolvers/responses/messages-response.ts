import { Message } from "src/app/models/domain/messenger/message";
import { Recipient } from "src/app/models/domain/messenger/recipient";
import { BaseResponse } from "./base-response";

export class MessagesResponse extends BaseResponse {
    messages: Message[];
    recipient: Recipient;
}