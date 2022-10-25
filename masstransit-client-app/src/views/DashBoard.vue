<template>
	<MassTransitNav />
	<div class="header">
		<div class="container">
			<div class="messageBox">
				<MessageBox />
			</div>
			<div class="chatterBox">
				<ChatterBox />
			</div>
		</div>
	</div>
</template>

<script>
	import masstransitChatHub from "@/hubs/masstransitChatHub";
	import MessageBox from "@/components/Chat/MessageBox.vue";
	import ChatterBox from "@/components/Chat/ChatterBox.vue";
	import MassTransitNav from "@/components/MassTransitNav.vue";
	import { useAuthStore } from "../stores/authStore";
	import { useChatStore } from "../stores/chatStore";

	export default {
		name: "Dashboard",
		components: {
			ChatterBox,
			MessageBox,
			MassTransitNav,
		},
		setup() {
			const auth = useAuthStore();
			const chat = useChatStore();
			return { chat, auth };
		},
		created() {
			if (this.auth.getLoginId === undefined) {
				this.$router.push("/login");
			}
		},
		mounted() {
			masstransitChatHub.start(this.auth.getToken).then(() => {
				masstransitChatHub.client.invoke("JoinGroup", "masstransit");
				masstransitChatHub.client.on("PublishChatMessage", async (message) => {
					this.chat.addMessage(message);
				});
			});
		},
	};
</script>

<style lang="scss" scoped>
	.container {
		background-color: #fff;
		border-radius: 10px;
		box-shadow: 0 14px 28px rgba(0, 0, 0, 0.25), 0 10px 10px rgba(0, 0, 0, 0.22);
		position: absolute;
		overflow: visible;
		width: 500px;
		max-width: 100%;
		height: 400px;
		top: 15%;
		right: 37%;
		display: flex;
		flex-direction: column;

		.messageBox {
			justify-content: space-between;
			align-items: baseline;
		}
		.chatterBox {
			position: absolute;
			top: 100%;
			padding: 10px;
		}
	}
</style>
