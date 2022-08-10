<template>
	<div
		v-for="message in getMessages"
		:key="message.messageId"
		class="message"
		:class="[
			message.username === $store.getters.getAuth.username ? 'me' : 'other',
		]"
	>
		{{ message.message }}
	</div>
</template>

<script>
	export default {
		name: "MessageBox",
		computed: {
			getMessages() {
				console.log(this.$store.getters.allMessages);
				return this.$store.getters.allMessages;
			},
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
