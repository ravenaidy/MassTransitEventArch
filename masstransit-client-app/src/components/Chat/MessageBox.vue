<template>
	<div
		v-for="message in getMessages"
		:key="message.messageId"
		class="message"
		:class="[message.username === this.auth.username ? 'me' : 'other']"
	>
		{{ message.message }}
	</div>
</template>

<script>
	import { useChatStore } from "../../stores/chatStore";
	import { useAuthStore } from "../../stores/authStore";

	export default {
		name: "MessageBox",
		computed: {
			getMessages() {
				console.log(this.chat.getAllMessages);
				return this.chat.getAllMessages;
			},
		},
		setup() {
			const chat = useChatStore();
			const auth = useAuthStore();
			return { chat, auth };
		},
		data() {
			return {
				messages: [],
			};
		},
	};
</script>

<style lang="scss" scoped>
	.message.me {
		background: #aaa;
		color: #fff;
		border-radius: 10px;
		padding: 1 rem;
		width: fit-content;
		margin: 10px;
	}
	.message.other {
		background: green;
		color: #fff;
		border-radius: 10px;
		padding: 1 rem;
		width: fit-content;
		margin: 10px;
	}
	.message.dark {
		background: #e9eaf6;
	}
</style>
