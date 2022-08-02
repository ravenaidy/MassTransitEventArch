<template>
	<MassTransitNavVue />
	<header class="header">
		<div class="container">			
			<div>
				<h1>Event-driven architecture code pen</h1>
				<p>
					Lorem ipsum dolor sit amet, consectetur adipisicing elit. Porro debitis unde sunt soluta. Officia, quaerat error corporis minus amet quo ad odio facilis aperiam corrupti deserunt iusto reprehenderit nesciunt minima?
				</p>
			</div>
			<img src="../assets/img/mt-logo-color.png" />
		</div>
	</header>
	<section>
		<div class="container">			
			<div class="box">
				<h2>Transport</h2>
				<p>
					Kafka, RabbitMQ, SignalR 
				</p>
			</div>
			<div class="box">
				<h2>Storage</h2>
				<p>
					MS SQL, Redis, Couchbase
				</p>
			</div>
			<div class="box">
				<h2>Libraries / Languages</h2>
				<p>
					C#, Python, Vuejs, Mediator, AutoMapper, Dapper, FastApi, Masstransit, Sass
				</p>
			</div>

		</div>

	</section>
</template>

<script>

import MassTransitNavVue from '@/components/MassTransitNav.vue'

export default {
	name: "Home",
	components: {
		MassTransitNavVue
	}
}

</script>

