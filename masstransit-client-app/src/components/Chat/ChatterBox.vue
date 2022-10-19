<template>
	<form @submit.prevent="sendMessage" ref="messageInput">
		<input type="text" placeholder="Write a message" v-model="message" />
		<button :disabled="message === ''">Send</button>
	</form>
</template>

<script>
	import masstransitChatHub from "@/hubs/masstransitChatHub";
	import { useAuthStore } from "../../stores/authStore";

	export default {
		name: "ChatterBox",
		data() {
			return {
				message: "",
			};
		},
		setup() {
			const auth = useAuthStore();
			return { auth };
		},
		methods: {
			async sendMessage() {
				const postMessage = {
					messageId: this.auth.getLoginId,
					message: this.message,
					username: this.auth.getUserName,
				};

				masstransitChatHub.client.invoke(
					"SendMessage",
					"masstransit",
					postMessage
				);
				this.resetFields();
			},
			resetFields() {
				Object.assign(this.$data, this.$options.data.call(this));
			},
		},
	};
</script>

<style lang="scss" scoped>
	form {
		padding: 5px opxs;
		display: flex;
		width: 500px;
		align-content: center;
		justify-content: flex-end;

		> input {
			margin: 5px;
			padding: 12px;
			width: 100%;
			border-radius: 10px;
			border-style: none;
			background-color: #eee;
			box-shadow: 0 14px 28px rgba(0, 0, 0, 0.25),
				0 10px 10px rgba(0, 0, 0, 0.22);
		}
		> input:focus {
			outline: none;
		}

		> button {
			margin: 5px;
			padding: 12px;
			border-radius: 10px;
			box-shadow: 0 14px 28px rgba(0, 0, 0, 0.25),
				0 10px 10px rgba(0, 0, 0, 0.22);
			letter-spacing: 1px;
			text-transform: uppercase;
			transition: transform 80ms ease-in;
			border-style: none;
			border: 1px solid #0151cc;
			background-color: #0151cc;
			color: #ffffff;
		}

		button:active {
			transform: scale(0.95);
		}

		> button:disabled {
			opacity: 0.5;
		}
	}
</style>
