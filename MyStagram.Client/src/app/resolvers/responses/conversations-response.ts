import { Conversation } from "src/app/models/helpers/messenger/conversation";
import { BaseResponse } from "./base-response";

export class ConversationsResponse extends BaseResponse {
    conversations: Conversation[];
}