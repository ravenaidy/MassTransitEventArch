<template>
	<div v-for="message in getMessages" :key="message.messageId" class="message">
		{{ message.message }}
	</div>
</template>

<script>
	export default {
		name: "MessageBox",
		computed: {
			getMessages() {
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
	.message {
		background: #aaa;
		color: #fff;
		border-radius: 10px;
		padding: 1 rem;
		width: fit-content;

		> h5 {
			margin: 0 0 0.5rem 0;
		}
	}
	.message.dark {
		background: #e9eaf6;
	}
</style>
