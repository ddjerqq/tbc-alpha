<script>
  import NavBar from "$lib/components/NavBar.svelte";
</script>

<svelte:head>
    <title>Home</title>
    <meta name="description" content="Svelte demo app"/>
</svelte:head>

<div class="w-full h-full bg-topo overflow-y-scroll flex flex-col justify-between px-5 bg-neutral-100">
    <main class="my-10 h-[calc(100% + 10rem)] flex flex-col justify-center align-middle items-center">
        <slot></slot>
    </main>

    <NavBar/>
</div>
